using Domain.AggregateRoots;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Exceptions;
using Infrastructure.Mappings;
using Infrastructure.Models;
using System.Text;
using TinyCsvParser;

namespace Infrastructure.Repositories
{
    public class TriangleRepository : ITriangleRepository
    {
        public List<Triangle> GetAll(string filePath)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CSVRowMapping csvMapper = new CSVRowMapping();
            CsvParser<CSVImportRow> csvParser = new CsvParser<CSVImportRow>(csvParserOptions, csvMapper);

            List<TinyCsvParser.Mapping.CsvMappingResult<CSVImportRow>>? csvRows = null;
            
            try
            {
                csvRows = csvParser
                         .ReadFromFile(filePath, Encoding.ASCII)
                         .ToList();
            }
            catch (IOException ex)
            {
                // Exception caught in console app.
                // Errors written out for user to see.is 
                throw new ImportException(new List<string>()
                {
                    ex.Message,
                });
            }

            var errors = csvRows.Select(x => x?.Error?.Value)
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (errors.Any())
            {
                // Exception caught in console app.
                // Errors written out for user to see.
                throw new ImportException(errors);
            }

            var triangleNames = csvRows
                .Select(x => x.Result.Name.Trim())
                .Distinct();

            var triangles = new List<Triangle>();

            foreach (var triangleName in triangleNames)
            {
                var triangleRows = csvRows.Where(x => x.Result.Name.Trim() == triangleName);
                var developmentYears = new List<DevelopmentYear>();

                foreach (var triangleRow in triangleRows)
                {
                    if (!DevelopmentYear.IsValid(triangleRow.Result.OriginYear, triangleRow.Result.Year))
                    {
                        // Todo: Add more detail to errors. Not enough time to do this.
                        errors.Add($"Row index: {triangleRow.RowIndex}");
                        continue;
                    }

                    developmentYears.Add(new DevelopmentYear(
                        originYear: triangleRow.Result.OriginYear,
                        year: triangleRow.Result.Year,
                        value: triangleRow.Result.Value));
                }

                triangles.Add(new Triangle(triangleName, developmentYears));
            }

            return triangles;
        }

        public void SaveCalculation(string filePath, TriangleCalculation calculation)
        {
            var csv = new StringBuilder();
            csv.AppendLine($"{calculation.EarliestOriginYear}, {calculation.NumberOfOriginYears}");

            foreach (var name in calculation.Triangles.Select(x => x.Name.Trim()))
            {
                var triangle = calculation.Triangles.First(x => x.Name.Trim() == name);
                var dataValues = new List<string>();
                dataValues.Add(name);

                foreach (var originYear in calculation.OriginYears)
                {
                    decimal accumulation = 0;

                    foreach (var developmentYear in originYear.DevelopmentYears)
                    {
                        var triangleDevelopmentYear = triangle.DevelopmentYears
                            .FirstOrDefault(x => x.OriginYear == originYear.Value && x.Year == developmentYear.Year);

                        accumulation += triangleDevelopmentYear?.Value ?? 0;
                        dataValues.Add(accumulation.ToString());
                    }
                }

                csv.AppendLine(string.Join(",", dataValues));
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
