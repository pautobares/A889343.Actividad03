using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889343.Actividad03
{
    class Asiento
    {
        public float SaldoDebe { set { saldoDebe = value; } get => saldoDebe; }
        public float SaldoHaber { set { saldoHaber = value; } get => saldoHaber; }
        public ArrayList Cuentas { set { cuentas = value; } get => cuentas; }
        public DateTime Fecha { set { fecha = value; } get => fecha; }
        public int NAsiento { set { nAsiento = value; } get => nAsiento; }

        private float saldoDebe = 0;
        private float saldoHaber = 0;
        private int nAsiento;
        private DateTime fecha;
        private ArrayList cuentas;

        public Asiento(DateTime fecha, int nAsiento)

        {
            this.cuentas = new ArrayList();
            this.fecha = fecha;
            this.nAsiento = nAsiento;
        }
        public void Add(Cuenta c)
        {
            if (cuentas.Count != 0)
            {
                if (cuentas.Contains(c))
                {
                    Console.WriteLine("Error, se quiere colocar dos veces la misma cuenta: \n" + c.ToString());
                    return;
                }
            }
            // Coinciden, la cuenta va con el saldo habitual? Si=>
            if (c.TipoSaldo)
            {
                SaldoDebe += c.Saldo;
            }
            else
            {
                SaldoHaber += c.Saldo;
            }
            cuentas.Add(c);
        }
        public bool estáBien()
        {
            return saldoDebe == saldoHaber;
        }
        public void reodenarCuentas()
        {
            int q = cuentas.Count;
            // Recorre las cuentas
            for (int i = 0; i < q; i++)
            {
                Cuenta cuenta = (Cuenta)cuentas[i];
                // Si es de saldo Acreedor => La tira al último
                if (!cuenta.TipoSaldo)
                {
                    // Lo Agrega nuevamente   
                    cuentas.Add(cuenta);
                    // Finalmente quita el repetido en la 1ra vez que apareció
                    cuentas.RemoveAt(i);
                }
            }
        }

        public override string ToString()
        {
            String s = fecha.ToString("dd/MM/yyyy");
            s += "\n";
            s += "-------------------------------" + NAsiento + "-------------------------------\n";
            s += String.Format("{0,40}|{1,5}{0,5}|{2,5}{0,5}", "", "D", "H") + "|\n";
            reodenarCuentas();
            foreach (Cuenta cuenta in cuentas)
            {
                if (cuenta.TipoSaldo)
                {
                    s += String.Format("{0,-40}", cuenta.Nombre);
                    s += "|" + String.Format("${0,-9:#.##}", cuenta.Saldo);
                    s += "|" + String.Format("{0,-10:#.##}", "");
                }
                else
                {
                    s += String.Format("{1,4}{0,-36}", cuenta.Nombre, "");
                    s += "|" + String.Format("{0,-10:#.##}", "");
                    s += "|" + String.Format("${0,-9:#.##}", cuenta.Saldo);
                }
                s += "|\n";
            }
            return s;
        }
        public bool nAsientoCorrecto(int nA, ArrayList asientos)
        {
            foreach (Asiento asiento in asientos)
            {
                if (asiento.NAsiento == nA)
                {
                    Console.WriteLine("Error, el numero de Asiento que quiere ingresar ya existe");
                    return false;
                }
            }
            return true;
        }
    }
}
