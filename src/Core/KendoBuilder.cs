using Core.Builders.Grid;

namespace Core
{
    /// <summary>
    /// Starting point to build Kendo UI elements markup
    /// </summary>
    /// <typeparam name="T">Type of data in elements</typeparam>
    public static class KendoBuilder<T>
    {
        /// <summary>
        /// Creates Grid element builder
        /// </summary>
        /// <returns></returns>
        public static KendoGridBuilder<T> Grid()
        {
            return new KendoGridBuilder<T>();
        }
    }
}