using BLL.IServices;
using DAL.Entities;
using School.views.Pages;
using SchoolBLL.IServices;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;

namespace School.views.UserControls
{
    public partial class FeesPage : UserControl
    {

        private readonly IFeesServices _service;
        private ObservableCollection<Fee> _fees = new();
        private IEnumerable<FeeType> _feeTypes;
        private Fee _selected;

        public FeesPage(IFeesServices service)
        {
            InitializeComponent();
            _service = service;
            dgFees.ItemsSource = _fees;
            _ = InitAsync();
        }

        private async Task InitAsync()
        {
            try
            {
                _feeTypes = await _service.GetAllFeeTypes();
                cbFeeType.ItemsSource = _feeTypes;
                await LoadFeesAsync();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "خطأ"); }
        }

        private async Task LoadFeesAsync()
        {
            var list = await _service.GetAll();
            _fees.Clear();
            foreach (var f in list) _fees.Add(f);
        }

        private void ClearFields_Click(object sender, RoutedEventArgs e)
        {
            cbStudent.SelectedItem = null;
            cbFeeType.SelectedItem = null;
            txtAmount.Text = "";
            _selected = null;
            dgFees.UnselectAll();
        }

        private async void SaveFee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbStudent.SelectedItem == null) { MessageBox.Show("اختر طالباً"); return; }
                if (cbFeeType.SelectedItem == null) { MessageBox.Show("اختر نوع الرسوم"); return; }

                // فرضا cbStudent.Value هو StudentId (إن لم يكن، استخرج Id من النص)
                var studentId = Convert.ToInt32(((dynamic)cbStudent.SelectedItem).Id);
                var feeTypeId = ((FeeType)cbFeeType.SelectedItem).FeeTypeId;
                var amount = decimal.Parse(txtAmount.Text.Trim());

                if (_selected == null)
                {
                    var fee = new Fee { StudentId = studentId, FeeTypeId = feeTypeId, Amount = amount, PaidAmount = 0 };
                    await _service.Add(fee);
                    MessageBox.Show("تمت الإضافة");
                }
                else
                {
                    _selected.StudentId = studentId;
                    _selected.FeeTypeId = feeTypeId;
                    _selected.Amount = amount;
                    await _service.Update(_selected);
                    MessageBox.Show("تم التعديل");
                }

                await LoadFeesAsync();
                ClearFields_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "خطأ"); }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e) {

            //await LoadFeesAsync();
            FeesAssignmentWindow feesAssignmentWindow = new FeesAssignmentWindow();
            feesAssignmentWindow.ShowDialog();
        }

        private CancellationTokenSource _cts;
        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            await Task.Delay(300, token).ContinueWith(t =>
            {
                if (t.IsCanceled) return;
                Dispatcher.InvokeAsync(async () =>
                {
                    var q = txtSearch.Text.Trim();
                    if (string.IsNullOrEmpty(q)) await LoadFeesAsync();
                    else
                    {
                        var res = await _service.Search(q);
                        _fees.Clear();
                        foreach (var f in res) _fees.Add(f);
                    }
                });
            }, token);
        }

        private void dgFees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selected = dgFees.SelectedItem as Fee;
            if (_selected != null)
            {
                // fill fields
                txtAmount.Text = _selected.Amount.ToString();
                // set fee type selection
                cbFeeType.SelectedItem = _feeTypes.FirstOrDefault(x => x.FeeTypeId == _selected.FeeTypeId);
                // set student selection if cbStudent items loaded
            }
        }

        private async void DeleteFee_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var id = (int)btn.Tag;
            var ok = MessageBox.Show("هل تريد حذف هذه الرسوم؟", "تأكيد", MessageBoxButton.YesNo);
            if (ok == MessageBoxResult.Yes)
            {
                await _service.Delete(id);
                await LoadFeesAsync();
            }
        }

        private async void EditFee_Click(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32(((Button)sender).Tag);
            var fee = await _service.Get(id);
            if (fee != null)
            {
                _selected = fee;
                // fill UI
                txtAmount.Text = fee.Amount.ToString();
                cbFeeType.SelectedItem = _feeTypes.FirstOrDefault(x => x.FeeTypeId == fee.FeeTypeId);
                // set student selection accordingly
                dgFees.SelectedItem = _selected;
            }
        }

        private async void MakePayment_Click(object sender, RoutedEventArgs e)
        {
            if (dgFees.SelectedItem == null) { MessageBox.Show("اختر رسماً للدفع"); return; }
            var fee = dgFees.SelectedItem as Fee;
            // simple input dialog: نطلب المبلغ من المستخدم
            var input = Microsoft.VisualBasic.Interaction.InputBox($"أدخل مبلغ الدفع (الباقي: {fee.RemainingAmount}):", "دفع", fee.RemainingAmount.ToString());
            if (decimal.TryParse(input, out decimal amount))
            {
                await _service.AddPayment(fee.FeeId, amount, null);
                await LoadFeesAsync();
                MessageBox.Show("تم تسجيل الدفعة");
            }
            else MessageBox.Show("مبلغ غير صالح");
        }

        private async void ShowPayments_Click(object sender, RoutedEventArgs e)
        {
            if (dgFees.SelectedItem == null) { MessageBox.Show("اختر رسماً لعرض دفعاته"); return; }
            //var fee = dgFees.SelectedItem as Fee;
            //var payments = await _service.GetPaymentsByFeeAsync(fee.FeeId);
            var sb = new System.Text.StringBuilder();
            //foreach (var p in payments) sb.AppendLine($"{p.PaymentDate:d} - {p.Amount} - {p.Notes}");
            MessageBox.Show(sb.Length == 0 ? "لا دفعات" : sb.ToString(), "دفعات الرسوم");
        }

        private async void PaymentsForFee_Click(object sender, RoutedEventArgs e)
        {
            var id = (int)((Button)sender).Tag;
            var payments = await _service.GetPaymentsByFee(id);
            var sb = new System.Text.StringBuilder();
            foreach (var p in payments) sb.AppendLine($"{p.PaymentDate:d} - {p.Amount} - {p.Notes}");
            MessageBox.Show(sb.Length == 0 ? "لا دفعات" : sb.ToString(), $"دفعات الرسوم #{id}");
        }
    }
}
