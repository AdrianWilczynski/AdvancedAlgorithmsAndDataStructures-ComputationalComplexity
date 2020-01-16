using Xunit;

namespace ThreeSATTests
{
    public class Tests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsUnsatisfiable(bool x)
        {
            var formula = (x || x || x) && (!x || !x || !x);
            Assert.False(formula);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, false, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, false)]
        public void IsSatisfiable(bool a, bool b, bool result)
        {
            var formula = (a || a || a) && (b || b || b);
            Assert.Equal(result, formula);
        }
    }
}
