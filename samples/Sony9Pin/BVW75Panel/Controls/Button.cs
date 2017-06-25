using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CueGraphics.Protocol.Sony9Pin
{
    public partial class Button : UserControl
    {
        #region Properties
            [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Bindable(true)]
            public Image Image
            {
                get { return button1.Image; }
                set { button1.Image = value; }
            }

            [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Bindable(true)]
            public Color ButtonColor
            {
                get { return button1.BackColor; }
                set { button1.BackColor = value; }
            }

            [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Bindable(true)]
            public override string Text
            {
                get { return label1.Text; }
                set { label1.Text = value; }
            }

            [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Bindable(true)]
            public Color BorderColor
            {
                get { return panel1.BackColor; }
                set { panel1.BackColor = value; }
            }

            [EditorBrowsable(EditorBrowsableState.Always), Browsable(true),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Bindable(true)]
            //public bool Lit
            //{
            //    get { return label1.Text; }
            //    set { label1.Text = value; }
            //}
        #endregion

        #region Events
            // Event that gets called on response
            public new event EventHandler Click;

            private void InvokeClick(EventArgs e)
            {
                var handler = Click;
                if (handler != null) handler(this, e);
            }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Button()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            InvokeClick(e);
        }

    }
}
