using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCDC_Manager
{
    public class PanelContainer : Output
    {

        private List<DCDC_Manager.Output> _panel;

        public List<DCDC_Manager.Output> Panel
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public void addPanel(IPanel panel)
        {
            throw new System.NotImplementedException();
        }

        public IPanel removePanel()
        {
            throw new System.NotImplementedException();
        }
    }
}