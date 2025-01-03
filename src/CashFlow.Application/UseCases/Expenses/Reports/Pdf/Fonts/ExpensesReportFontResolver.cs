﻿using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
public class ExpensesReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string fontName)
    {
        var stream = ReadFontFile(fontName);

        if (stream is null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }

        var length = (int)stream!.Length;
        var data = new byte[length];

        stream.Read(buffer : data, offset: 0, count: length);

        return data;

    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string fontName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var path = $"CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts.{fontName}.ttf";

        return assembly.GetManifestResourceStream(path);
    }
}
