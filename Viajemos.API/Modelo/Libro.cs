using DevExpress.Xpo;
using System;

namespace ViajemosBK.Modelo
{
    [Persistent("libros")]
    public class Libro : XPObject
    {
        public Libro() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Libro(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        Editorial editorial;
        string numeroPaginas;
        string sinopsis;
        string título;
        int iSBN;

        [Size(13)]
        [Nullable(false)]
        public int ISBN
        {
            get => iSBN;
            set => SetPropertyValue(nameof(ISBN), ref iSBN, value);
        }


        [Size(45)]
        public string Título
        {
            get => título;
            set => SetPropertyValue(nameof(Título), ref título, value);
        }

        [Size(800)]
        public string Sinopsis
        {
            get => sinopsis;
            set => SetPropertyValue(nameof(Sinopsis), ref sinopsis, value);
        }

        [Size(45)]
        public string NumeroPaginas
        {
            get => numeroPaginas;
            set => SetPropertyValue(nameof(NumeroPaginas), ref numeroPaginas, value);
        }

        [Association("A-L")]
        public XPCollection<Autor> Autores
        {
            get => GetCollection<Autor>(nameof(Autores));
        }

        //[Nullable(true)]
        [Association("L-E")]
        public Editorial Editorial
        {
            get => editorial;
            set => SetPropertyValue(nameof(Editorial), ref editorial, value);
        }


    }

}