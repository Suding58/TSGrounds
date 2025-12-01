using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSGrounds
{
    public class DvgUtility
    {
        public static void HideUnwantedColumns(DataGridView dvg, bool showall = false, bool clearHeader = true)
        {
            if (clearHeader && (dvg.DataSource == null || dvg.Rows.Count == 0))
            {
                dvg.Columns.Clear();
                dvg.Refresh();
                return;
            }

            if (dvg.Columns.Count > 0)
            {
                foreach (DataGridViewColumn column in dvg.Columns)
                {
                    if (column.Name != "Id")
                    {
                        column.Visible = false;
                    }
                    else
                    {
                        column.Visible = true;
                    }
                    column.HeaderText = column.HeaderText.ToUpper();
                }

                if (dvg.Columns.Contains("Id"))
                {
                    dvg.Columns["Id"].HeaderText = "ID";
                    dvg.Columns["Id"].Width = 50;
                }
            }
        }

    }
}
