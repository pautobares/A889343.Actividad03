using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889343.Actividad03
{
    class Cuenta
    {
        public int Codigo { get { return codigo; } set { this.codigo = value; } }
        public String Nombre { get => nombre; set { nombre = value; } }
        public int TipoCuenta { get => tipoCuenta; set { tipoCuenta = value; } }
        public float Saldo { get => saldo; set { saldo = value; } }
        public bool TipoSaldo { get => tipoSaldo; set { tipoSaldo = value; } }
        private int codigo;
        private int tipoCuenta;
        private String nombre;
        private float saldo;
        private bool tipoSaldo;
        // Tipo Saldo => True: Debe | Deudor
        // Tipo Saldo => False: Haber | Acreedor


        public Cuenta(int codigo, string nombre, int tipoCuenta)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.tipoCuenta = tipoCuenta;
            this.tipoSaldo = tipoCuenta == 0 || tipoCuenta == 3;
            this.saldo = 0;
        }

        public Cuenta() { }

        public Cuenta(int codigo, string nombre, int tipoCuenta, float saldo, bool tipoSaldo)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.tipoCuenta = tipoCuenta;
            this.saldo = saldo;
            this.tipoSaldo = tipoSaldo;
        }

        public override string ToString()
        {
            String s = "" + codigo;
            s += "\t" + nombre;
            s += "\t" + toTipoCuenta() + "\t";
            if (saldo != 0.0f)
            {
                s += "$" + saldo;
                s += ": ";
                s += TipoSaldo ? "Deudor" : "Acreedor";
            }

            return s;
        }
        public String toTipoCuenta()

        {
            String s = "";
            switch (tipoCuenta)
            {
                case 0:
                    s = "Activo";
                    break;
                case 1:
                    s = "Pasivo";
                    break;
                case 2:
                    s = "Patrimonio Neto";
                    break;
                default:
                    Console.WriteLine("En serio, que fallo?", tipoCuenta);
                    Console.WriteLine(tipoCuenta == null ? "Con razon" : "Tiene algo");
                    Console.WriteLine(tipoCuenta.ToString());
                    Console.WriteLine(tipoCuenta);
                    s = "Error";
                    break;
            }
            return s;
        }
        public String toTipoCuenta(int tCuenta)
        {
            String s = "";
            switch (tCuenta)
            {
                case 0:
                    s = "Activo";
                    break;
                case 1:
                    s = "Pasivo";
                    break;
                case 2:
                    s = "Patrimonio Neto";
                    break;
                default:
                    s = "Error";
                    break;
            }
            return s;
        }
        public int toTipoCuenta(string s)
        {
            if (s == "Activo")
            {
                return 0;
            }
            else
            {
                if (s == "Pasivo")
                {
                    return 1;
                }
                else { return 2; }
            }
        }
    }
}
