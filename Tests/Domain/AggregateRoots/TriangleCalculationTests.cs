using Domain.AggregateRoots;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Tests.Domain.AggregateRoots
{
    public class TriangleCalculationTests
    {
        TriangleCalculation GenerateValidTriangle()
        {
            var compTriangle = new Triangle("comp", new List<DevelopmentYear>()
            {
                new DevelopmentYear(1992, 1992, 110),
                new DevelopmentYear(1992, 1993, 170),
                new DevelopmentYear(1993, 1993, 200),
            });

            var nonCompTriangle = new Triangle("comp", new List<DevelopmentYear>()
            {
                new DevelopmentYear(1990, 1990, 45.2M),
                new DevelopmentYear(1990, 1991, 64.8M),
                new DevelopmentYear(1990, 1993, 37),

                new DevelopmentYear(1991, 1991, 50),
                new DevelopmentYear(1991, 1992, 75),
                new DevelopmentYear(1991, 1993, 25),

                new DevelopmentYear(1992, 1992, 55),
                new DevelopmentYear(1992, 1993, 85),

                new DevelopmentYear(1993, 1993, 100),
            });

            return new TriangleCalculation(new List<Triangle>()
            {
                compTriangle,
                nonCompTriangle
            });
        }

        [Fact]
        public void TotalDevelopmentYears_Returns_Expected_Value()
        {
            var triangleCalculation = GenerateValidTriangle();
            triangleCalculation.TotalDevelopmentalYears.Should().Be(10);
        }

        [Fact]
        public void NumberOfOriginYears_Returns_Expected_Value()
        {
            var triangleCalculation = GenerateValidTriangle();
            triangleCalculation.NumberOfOriginYears.Should().Be(4);
        }

        [Fact]
        public void EarliestOriginYear_Returns_Expected_Value()
        {
            var triangleCalculation = GenerateValidTriangle();
            triangleCalculation.EarliestOriginYear.Should().Be(1990);
        }
    }
}
