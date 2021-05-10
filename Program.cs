using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace A889343.Actividad03

{
    class Menu
    {
        static void Main(string[] args)
        {
            ArrayList libroDiario = new ArrayList();
            String ruta = "D:\\Plan de cuentas.txt";
            ArrayList planDeCuentas = toPlan(ruta);
            int opc = -1;
            Console.WriteLine("Bienvenidos al sistema");
            String rutaDiario = "D:\\Diario.txt";
            StreamWriter sw;
            StreamReader sr;
            while (opc != 0)
            {
                opc = mostrarMenu();
                switch (opc)
                {
                    case 0:
                        Console.WriteLine("Muchas gracias por usar el programa");
                        break;
                    case 1:
                        sw = devolverArchivoEscritura(rutaDiario, true);
                        int nAsiento = mayorQue(0, "Ingrese el numero del Asiento: ");
                        int dia, mes, año;
                        Asiento asiento;
                        // Validación de del codigo correcto
                        while (!nAsientoCorrecto(nAsiento, libroDiario))
                        {
                            Console.WriteLine("Por favor intente otro numero");
                            nAsiento = mayorQue(0, "Ingrese el numero del Asiento: ");
                        }
                        dia = valorEntre(1, 30, "Ingrese el día: ");
                        mes = valorEntre(1, 12, "Ingrese el mes: ");
                        año = valorEntre(1990, DateTime.Today.Year, "Ingrese el numero de año: ");
                        DateTime fecha = new DateTime(año, mes, dia);
                        asiento = new Asiento(fecha, nAsiento);
                        while (asiento.SaldoDebe == 0 || !asiento.estáBien())
                        {
                            Console.WriteLine("Desea añadir cuenta? si (0) / no (1)?");
                            int elección = valorEntre(0, 1, "Ingrese la elección: ");
                            if (elección == 1)
                            {
                                if (asiento.SaldoDebe != 0 && asiento.estáBien())
                                {
                                    Console.WriteLine("Excelente, gracias");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Lo siento, pero el asiento está mal");
                                    float dif = (asiento.SaldoDebe - asiento.SaldoHaber);
                                    if (dif > 0)
                                    {
                                        Console.WriteLine("Hay una diferencia de $" + dif + " . En el debe");
                                    }
                                    else
                                    {
                                        if (dif < 0) { Console.WriteLine("Hay una diferencia de $" + (-dif) + " . En el haber"); }
                                    }
                                }
                            }
                            else
                            {
                                Cuenta c;
                                int codigo=0;
                                bool flag = true;

                                while (flag)
                                {
                                    codigo = mayorQue(0, "Ingrese el codigo de la cuenta: ");
                                    foreach (Cuenta cuenta in planDeCuentas)
                                    {
                                        if (cuenta.Codigo == codigo)
                                        {
                                            flag = false;
                                            break;

                                        }
                                    }
                                }
                                float saldo = MayorQue(0, "Ingrese el monto: ");
                                bool isSaldoDeudor = valorEntre(0, 1, "Ingrese 1 si el saldo es Acreedor y 0 sino: ") == 0;
                                for (int i = 0; i < planDeCuentas.Count; i++)
                                {
                                    Cuenta cuenta = (Cuenta)planDeCuentas[i];
                                    if (cuenta.Codigo == codigo)
                                    {
                                        c = new Cuenta(cuenta.Codigo, cuenta.Nombre, cuenta.TipoCuenta, saldo, isSaldoDeudor);
                                        asiento.Add(c);
                                    }
                                }

                            }
                        }
                        sw.WriteLine(asiento.ToString());
                        Console.WriteLine(asiento.ToString());
                        sw.Close();
                        break;
                    case 2:
                        sr = devolverArchivoLectura(rutaDiario);
                        leerArchivo(sr);
                        sr.Close();
                        break;
                    case 3:
                        sr = devolverArchivoLectura(ruta);
                        leerArchivo(sr);
                        sr.Close();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }
        }
        public static int mostrarMenu()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("         Menu de Opciones         ");
            Console.WriteLine("1) Ingresar un nuevo Asiento");
            Console.WriteLine("2) Mostrar Archivo Diario.txt");
            Console.WriteLine("3) Mostrar Archivo de Cuentas");
            Console.WriteLine("0) Salir");
            int x = valorEntre(0, 4);
            return x;
        }
        public static float retornarFlotante(String msg = "Ingrese un numero: ")
        {
            float n;
            while (true)
            {
                try
                {
                    Console.WriteLine(msg);
                    n = (float)Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Error, intente nuevamente");
                    continue;
                }
                break;
            }
            return n;

        }
        public static int retornarEntero(String msg = "Ingrese un numero: ")
        {
            int n;
            while (true)
            {
                try
                {
                    Console.WriteLine(msg);
                    n = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Error, intente nuevamente");
                    continue;
                }
                break;
            }
            return n;
        }
        public static int mayorQue(int max, String mensaje = "Ingrese un numero")
        {
            int n;
            n = retornarEntero(mensaje);
            while (n < max)
            {
                Console.WriteLine("Error, el numero debe ser mayor que " + max + "");
                n = retornarEntero(mensaje);
            }
            return n;
        }
        public static float MayorQue(int max, String mensaje = "Ingrese un numero")
        {
            float n;
            n = retornarFlotante(mensaje);
            while (n < max)
            {
                Console.WriteLine("Error, el numero debe ser mayor que " + max + "");
                n = retornarFlotante(mensaje);
            }
            return n;
        }
        public static int valorEntre(int limiteInferior, int limiteSuperior, String mensaje = "Ingrese un numero: ")
        {
            int n;

            do
            {
                n = retornarEntero(mensaje);
                if (n >= limiteInferior && n <= limiteSuperior)
                {
                    return n;
                }
                Console.WriteLine("Error, el numero debe ser mayor que " + limiteInferior + " y menor que " + limiteSuperior);
            } while (n > limiteSuperior || n < limiteInferior);
            return n;
        }
        public static bool nAsientoCorrecto(int nA, ArrayList asientos)
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
        public static ArrayList toArray(String r)
        {
            ArrayList lista = new ArrayList(3);
            int c = 0;
            String s = "";
            foreach (char car in r)
            {
                if (car == '|')
                {
                    if (c == 0)
                    {

                        try
                        {
                            int x = Convert.ToUInt16(s);
                            lista.Add(x);
                            c++;
                            s = "";
                            continue;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Error, valor erroneo: " + s + " no es un número entero");
                            return null;
                        }
                    }
                    lista.Add(s);
                    s = "";
                    continue; // Evita que el | se encuentre en la palabra
                }
                s += car;
            }
            lista.Add(s);
            Console.WriteLine(s);
            return lista;
        }

        private static void leerArchivo(StreamReader sr)
        {
            String line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            Console.WriteLine(line);
            Thread.Sleep(100);
            sr.Close();
        }
        private static StreamReader devolverArchivoLectura(String ruta)
        {
            StreamReader sr;
            while (true)
            {

                try
                {
                    if (!File.Exists(ruta)) { Console.WriteLine("El archivo en la ruta no existe"); }
                    sr = new StreamReader(ruta);
                }
                catch (Exception)
                {
                    Console.WriteLine("La ruta es Erronea o Archivo Inexistente");
                    Console.WriteLine("No se va a poder hacer la Lectura");
                    Console.WriteLine("Ingrese el nombre de la ruta: ");
                    ruta = Console.ReadLine();
                    continue;
                }
                break;
            }
            return sr;

        }
        public static StreamWriter devolverArchivoEscritura(String ruta, bool append = false)
        {
            StreamWriter sr;
            while (true)
            {
                try
                {
                    sr = new StreamWriter(ruta, append);
                }
                catch (Exception)
                {
                    Console.WriteLine("La ruta es Erronea o Archivo Inexistente");
                    Console.WriteLine("No se va a poder realizar la Escritura");
                    Console.WriteLine("Ingrese el nombre de la ruta: ");
                    ruta = Console.ReadLine();
                    continue;
                }
                break;
            }
            return sr;

        }
        public static ArrayList toPlan(String ruta)
        {
            ArrayList planDeCuentas = new ArrayList();
            StreamReader sr = devolverArchivoLectura(ruta);
            String linea = sr.ReadLine();
            linea = sr.ReadLine();
            while (linea != null)
            {
                Cuenta cuenta = new Cuenta();
                int cod = 0, tc = -1;
                String nombre = "";
                int c = 0;
                String s = "";
                foreach (char car in linea)
                {
                    if (car == '|')
                    {
                        if (c == 0)
                        {

                            try
                            {
                                int x = Convert.ToUInt16(s);
                                cod = x;
                                c++;
                                s = "";
                                continue;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error, valor erroneo: " + s + " no es un número entero");
                                return null;
                            }
                        }
                        else
                        {
                            if (c == 1) { nombre = s; c++; }
                        }
                        tc = cuenta.toTipoCuenta(s);
                        s = "";
                        continue; // Evita que el | se encuentre en la palabra
                    }
                    s += car;
                }
                cuenta = new Cuenta(cod, nombre, tc);
                planDeCuentas.Add(cuenta);
                linea = sr.ReadLine();
            }
            return planDeCuentas;
        }
        public static void mostrar()
        {

        }
    }
}