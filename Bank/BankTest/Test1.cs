using Bank;
using static System.Net.Mime.MediaTypeNames;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Client_with_0_balance()
        {


            Assert.ThrowsException<ArgumentException>(() => new Konto("Qwerty", -200));

        }

        [TestMethod]
        public void Wplata_Kwota_0()
        {
            Konto K1 = new Konto("Qwerty", 500);

            Assert.ThrowsException<ArgumentException>(() => K1.Wplata(-1000));

        }

        [TestMethod]

        public void Wyplata_Kwota_0()
        {
            Konto K1 = new Konto("Qwerty", 500);

            Assert.ThrowsException<ArgumentException>(() => K1.Wyplata(1000));

        }

        [TestMethod]

        public void ZablokowaneKonto__Wplata()
        {
            Konto K1 = new Konto("Qwerty", 500);

            K1.Zablokowane = true;

            Assert.ThrowsException<ArgumentException>(() => K1.Wplata(1000));

        }

        [TestMethod]

        public void ZablokowaneKonto__Wyplata()
        {
            Konto K1 = new Konto("Qwerty", 500);

            K1.Zablokowane = true;

            Assert.ThrowsException<ArgumentException>(() => K1.Wyplata(1000));

        }


    }
}