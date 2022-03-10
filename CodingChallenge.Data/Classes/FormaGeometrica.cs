/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodingChallenge.Data.Classes
{
    public abstract class FormaGeometrica
    {
        #region Formas

        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;
        

        #endregion

        protected readonly decimal _lado;

        public int Tipo { get; set; }

        public FormaGeometrica(int tipo, decimal ancho)
        {
            Tipo = tipo;
            _lado = ancho;
        }

        public FormaGeometrica(decimal ancho)
        {
            _lado = ancho;
        }

        public static string Imprimir(List<FormaGeometrica> formas, Languages idioma)
        {
            LanguageManager.SetLanguage(idioma);
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                sb.Append(Resource.ListaVacia);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER                
                sb.Append(Resource.ReporteTitulo);

                var numeroCuadrados = formas.Where((p) => p is Cuadrado).Count();
                var numeroCirculos = formas.Where((p) => p is Circulo).Count(); 
                var numeroTriangulos = formas.Where((p) => p is TrianguloEquilatero).Count();
                var totalNumeroFormas = numeroCirculos + numeroCuadrados + numeroTriangulos;

                var areaCuadrados = formas.Where((p) => p is Cuadrado).Sum((c) => c.CalcularArea());
                var areaCirculos = formas.Where((p) => p is Circulo).Sum((c) => c.CalcularArea()); ;
                var areaTriangulos = formas.Where((p) => p is TrianguloEquilatero).Sum((c) => c.CalcularArea());
                var totalAreaFormas = areaCirculos + areaCuadrados + areaTriangulos;

                var perimetroCuadrados = formas.Where((p) => p is Cuadrado).Sum((c) => c.CalcularPerimetro());
                var perimetroCirculos = formas.Where((p) => p is Circulo).Sum((c) => c.CalcularPerimetro());
                var perimetroTriangulos = formas.Where((p) => p is TrianguloEquilatero).Sum((c) => c.CalcularPerimetro());
                var totalPerimetroFormas = perimetroCirculos + perimetroCuadrados + perimetroTriangulos;

                if (numeroCuadrados > 0) sb.Append($"{numeroCuadrados} {(numeroCuadrados > 1 ? Resource.Cuadrados : Resource.Cuadrado)} | Area {areaCuadrados:#.##} | {Resource.Perimetro} {perimetroCuadrados:#.##} <br/>");
                if (numeroCirculos > 0) sb.Append($"{numeroCirculos} {(numeroCirculos > 1 ? Resource.Circulos : Resource.Circulo)} | Area {areaCirculos:#.##} | {Resource.Perimetro} {perimetroCirculos:#.##} <br/>");
                if (numeroTriangulos > 0) sb.Append($"{numeroTriangulos} {(numeroTriangulos > 1 ? Resource.Triangulos : Resource.Triangulo)} | Area {areaTriangulos:#.##} | {Resource.Perimetro} {perimetroTriangulos:#.##} <br/>");
                

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append($"{totalNumeroFormas} {Resource.Formas} ");
                sb.Append($"{Resource.Perimetro} {totalPerimetroFormas.ToString("#.##")} ");
                sb.Append($"Area {totalAreaFormas.ToString("#.##")}");
            }

            return sb.ToString();
        }

        
        //private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        //{
        //    if (cantidad > 0)
        //    {
        //        if (idioma == Castellano)
        //            return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

        //        return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
        //    }

        //    return string.Empty;
        //}

        //private static string TraducirForma(int tipo, int cantidad, int idioma)
        //{
            
        //    switch (tipo)
        //    {
        //        case Cuadrado:
        //            if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
        //            else return cantidad == 1 ? "Square" : "Squares";
        //        case Circulo:
        //            if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
        //            else return cantidad == 1 ? "Circle" : "Circles";
        //        case TrianguloEquilatero:
        //            if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
        //            else return cantidad == 1 ? "Triangle" : "Triangles";
        //    }

        //    return string.Empty;
        //}

        public static string GetCuadradoTraduccion(Languages lang)
        {
            LanguageManager.SetLanguage(lang);
            return Resource.Cuadrado;
        }
   

        public abstract decimal CalcularArea();
        public abstract decimal CalcularPerimetro();        
    }


    
}
