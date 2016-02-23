using System.Web.Script.Serialization;

namespace WpfCalculos
{
    public class Operacion
    {
        public Operacion(int op1, int op2)
        {
            Op1 = op1;
            Op2 = op2;
        }

        public int Op1 { get; set; }
        public int Op2 { get; set; }

        public override string ToString()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }
    }
}