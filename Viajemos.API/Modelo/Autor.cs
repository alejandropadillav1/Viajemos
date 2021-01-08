using DevExpress.Xpo;
using System;

namespace ViajemosBK.Modelo
{

    [Persistent("autores")]
    public class Autor : XPObject
    {
        public Autor() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Autor(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        string apellidos;
        string nombre;

        [Size(45)]
        public string Nombre
        {
            get => nombre;
            set => SetPropertyValue(nameof(Nombre), ref nombre, value);
        }

        [Size(45)]
        
        public string Apellidos
        {
            get => apellidos;
            set => SetPropertyValue(nameof(Apellidos), ref apellidos, value);
        }

        [Association("A-L")]
        public XPCollection<Libro> Libros
        {
            get => GetCollection<Libro>(nameof(Libros));
        }

    }

}