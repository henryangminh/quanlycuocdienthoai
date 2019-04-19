using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlycuocdienthoai_win.BUS
{
    public class Shared
    {
        public void ClearDataGridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }
        /*
        private void Update(Object T)
        {
            db.Entry(T).State = EntityState.Modified;
        }
        */
        public void CheckMinMaxValueOfNumericUpDown(NumericUpDown numericUpDown)
        {
            if (numericUpDown.Value < numericUpDown.Minimum)
            {
                numericUpDown.Value = numericUpDown.Minimum;
            }

            if (numericUpDown.Value > numericUpDown.Maximum)
            {
                numericUpDown.Value = numericUpDown.Maximum;
            }
        }

        /// <summary>
        /// Kiểm tra toàn bộ ký tự trong chuỗi chỉ chứa số
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CheckDigital(string text)
        {
            return text.All(Char.IsDigit);
        }
    }
}
