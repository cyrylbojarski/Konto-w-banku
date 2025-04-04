using Bank;

class Program
{
    static void Main()
    {

        KontoLimit kontoLimit = new KontoLimit("Imie Nazwisko", 5000, 500);
        Console.WriteLine("Saldo początkowe: " + kontoLimit.Bilans);

        kontoLimit.Wplata(500);
        Console.WriteLine("Saldo po wpłacie: " + kontoLimit.Bilans);

        kontoLimit.Wyplata(30);
        Console.WriteLine("Saldo po wypłacie: " + kontoLimit.Bilans);

        kontoLimit.Wyplata(45);
        Console.WriteLine("Saldo po próbie wypłaty: " + kontoLimit.Bilans);

        kontoLimit.OdblokujKonto();
        Console.WriteLine("Saldo po odblokowaniu: " + kontoLimit.Bilans);


    }
}
