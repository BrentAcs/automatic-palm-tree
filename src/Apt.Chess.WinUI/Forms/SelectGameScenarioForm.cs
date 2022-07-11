using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apt.Chess.Core.Models;

namespace Apt.Chess.WinUI.Forms
{
   public partial class SelectGameScenarioForm : Form
   {
      public SelectGameScenarioForm()
      {
         InitializeComponent();
      }

      public GameScenario SelectedGameScenario => (GameScenario) scenarioComboBox.SelectedValue;

      private void SelectGameScenarioForm_Load(object sender, EventArgs e)
      {
         scenarioComboBox.DisplayMember = "Description";
         scenarioComboBox.ValueMember = "Value";
         scenarioComboBox.DataSource = Enum.GetValues(typeof(GameScenario))
            .Cast<Enum>()
            .Select(value => new
            {
               (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
               value
            })
            .OrderBy(item => item.value)
            .ToList();

         scenarioComboBox.SelectedValue = (GameScenario)Settings.Default.LastGameScenario;
      }

      private void SelectGameScenarioForm_FormClosed(object sender, FormClosedEventArgs e)
      {
         Settings.Default.LastGameScenario = (int) scenarioComboBox.SelectedValue;
         Settings.Default.Save();
      }
   }
}
