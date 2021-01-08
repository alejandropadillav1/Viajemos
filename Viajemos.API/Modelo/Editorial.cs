using DevExpress.Xpo;
using System;

namespace ViajemosBK.Modelo
{
    [Persistent("editoriales")]
    public class Editorial : XPObject
    {
        object objeto = new object();
        public Editorial() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Editorial(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        string sede;
        string nombre;

        [Size(45)]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }

        [Size(45)]
        public string Sede
        {
            get => sede;
            set => SetPropertyValue(nameof(Sede), ref sede, value);
        }

        [Association("L-E")]
        public XPCollection<Libro> Libros
        {

            get
            {
                lock (objeto)
                    return GetCollection<Libro>(nameof(Libros));
            }
        }


    }

}