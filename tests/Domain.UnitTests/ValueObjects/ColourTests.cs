﻿using CargoCoordinationPlatform.Domain.Exceptions;
using CargoCoordinationPlatform.Domain.ValueObjects;
using NUnit.Framework;
using Shouldly;

namespace CargoCoordinationPlatform.Domain.UnitTests.ValueObjects;

public class ColourTests
{
    [Test]
    public void ShouldReturnCorrectColourCode()
    {
        var code = "#FFFFFF";

        var colour = Colour.From(code);

        colour.Code.ShouldBe(code);
    }

    [Test]
    public void ToStringReturnsCode()
    {
        var colour = Colour.White;

        colour.ToString().ShouldBe(colour.Code);
    }

    [Test]
    public void ShouldPerformImplicitConversionToColourCodeString()
    {
        string code = Colour.White;

        code.ShouldBe("#FFFFFF");
    }

    [Test]
    public void ShouldPerformExplicitConversionGivenSupportedColourCode()
    {
        var colour = (Colour)"#FFFFFF";

        colour.ShouldBe(Colour.White);
    }

    [Test]
    public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
    {
        Should.Throw<UnsupportedColourException>(() => Colour.From("##FF33CC"));
    }
}
