using System;

namespace Bank
{
    public class Konto
    {
        protected string klient;
        protected decimal bilans;
        protected bool zablokowane = false;

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            this.zablokowane = false;
            this.klient = klient;
            if (bilansNaStart >= 0)
            {
                bilans = bilansNaStart;
            }
            else
            {
                throw new ArgumentException("Bilans < 0");
            }
        }

        public string Klient
        {
            get { return klient; }
        }

        public decimal Bilans
        {
            get { return bilans; }
        }

        public bool Zablokowane
        {
            get { return zablokowane; }
            set { zablokowane = value; }
        }

        public virtual void Wplata(decimal kwota)
        {
            if (kwota > 0 && !zablokowane)
            {
                bilans += kwota;
            }
            else
            {
                throw new ArgumentException("Kwota musi być większa od 0 i konto nie może być zablokowane.");
            }
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (kwota > 0 && kwota <= bilans && !zablokowane)
            {
                bilans -= kwota;
            }
            else
            {
                throw new ArgumentException("Nieprawidłowa kwota wypłaty lub konto zablokowane.");
            }
        }

        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;
        }
    }

    public class KontoPlus : Konto
    {
        private decimal limit;

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 100)
            : base(klient, bilansNaStart)
        {
            this.limit = limit;
        }

        public decimal Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public decimal Bilance
        {
            get { return bilans; }
            set { bilans = value; }
        }

        public override void Wplata(decimal kwota)
        {
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota musi być większa niż 0");
            }

            bilans += kwota;

            if (bilans > 0 && zablokowane)
            {
                zablokowane = false;
            }
        }

        public override void Wyplata(decimal kwota)
        {
            if (kwota <= 0)
            {
                throw new ArgumentException("Kwota musi być większa niż 0");
            }

            if (zablokowane)
            {
                throw new ArgumentException("Konto jest zablokowane.");
            }

            if (kwota <= bilans)
            {
                bilans -= kwota;
            }
            else if (kwota <= bilans + limit)
            {
                bilans -= kwota;
                zablokowane = true;
            }
            else
            {
                throw new ArgumentException("Brak wystarczających środków");
            }
        }
    }

    public class KontoLimit
    {
        private Konto konto;
        private decimal limit;

        public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 100)
        {
            konto = new Konto(klient, bilansNaStart);
            this.limit = limit;
        }

        public string Nazwa
        {
            get { return konto.Klient; }
        }

        public decimal Bilans
        {
            get { return konto.Bilans; }
        }

        public bool Zablokowane
        {
            get { return konto.Zablokowane; }
            set { konto.Zablokowane = value; }
        }

        public decimal Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0");

            konto.Wplata(kwota);

            if (konto.Bilans > 0 && konto.Zablokowane)
            {
                konto.Zablokowane = false;
            }
        }

        public void Wyplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota musi być większa niż 0");

            if (konto.Zablokowane)
                throw new ArgumentException("Konto jest zablokowane.");

            if (kwota <= konto.Bilans)
            {
                konto.Wyplata(kwota);
            }
            else if (kwota <= konto.Bilans + limit)
            {
                konto.Wyplata(kwota);
                konto.Zablokowane = true;
            }
            else
            {
                throw new ArgumentException("Brak wystarczających środków");
            }
        }

        public void BlokujKonto()
        {
            konto.Zablokowane = true;
        }

        public void OdblokujKonto()
        {
            if (konto.Bilans > 0)
                konto.Zablokowane = false;
            else
                throw new ArgumentException("Konto nie może być odblokowane, gdy saldo jest równe 0 lub ujemne.");
        }
    }
}
