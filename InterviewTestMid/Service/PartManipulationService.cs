using InterviewTestMid.Model;
using System.Text.Json;
using InterviewTestMid.Interface;

namespace InterviewTestMid.Service
{
    public class PartManipulationService : IPartManipulationService
	{
        private readonly string _jsonDataFileName;

        //Left as default to create all json files in Data folder under current Directory - bin/{debug||release}/net8.0/Data.
        private static string _dataFilePath = @"Data";

        private readonly ILogger _logger;
        private readonly IFileSystem _fileSystem;

        public PartManipulationService(ILogger logger, IFileSystem fileSystem)
        {
            _logger = logger;
            _fileSystem = fileSystem;
            _jsonDataFileName = "SampleData.json";
        }

        public List<Part>? ListAndLogPartsMaterialDesc(string materialDescription)
        {
            List<Part>? parts = LoadBaseJsonPartsData();

            if (parts?.Count > 0)
            {
                //LINQ query to list of all the material descriptions for the "FOIL" part
                var foilDesc = parts?
                .Where(p => p.PartDesc == materialDescription)
                .SelectMany(p => p.Materials)
                .Select(m => m.Material?.LookDesc)
                .ToList();

            _logger.WriteCSV(foilDesc);

            return parts;
                
            }

            return null;
        }

        public List<Part>? LoadBaseJsonPartsData()
        {
            try
            {
                var sourceFilePath = Path.Combine(_dataFilePath, _jsonDataFileName);
                
                string jsonData = _fileSystem.ReadAllText(sourceFilePath);

                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    _logger.WriteLogMessage("JSON data is null or empty.");
                    return null;
                }

                var parts = JsonSerializer.Deserialize<List<Part>>(jsonData);

                return parts;
            }
            catch (Exception ex)
            {
                _logger.WriteErrorMessage(ex);

                throw;
            }
        }

        public Part ModifyPartWeight(Part part, double newPartWeight)
        {

            if (part.PartWeight.Value != newPartWeight)
            {
                part.PartWeight.Value = newPartWeight;
                _logger.WriteLogMessage($"PartWeight value of part with PartId {part.PartId} has been changed to {newPartWeight}");
            }
            else
            {
                _logger.WriteLogMessage($"Weight is already equals to new weight");
            }

            return part;
        }

        public bool CreateNewPartJsonFile(List<Part> parts)
        {
            bool isNewJsonCreated = false;
            try
            {

                if (!Directory.Exists(_dataFilePath))
                {
                    Directory.CreateDirectory(_dataFilePath);
                }
                string destinationFilePath = Path.Combine(_dataFilePath, $"NewPartData_{DateTime.Now:yyyyMMddHHmmss}.json");
                string jsonString = string.Empty;

                foreach (var part in parts)
                {
                    jsonString = JsonSerializer.Serialize(part, new JsonSerializerOptions { WriteIndented = true });
                    _fileSystem.AppendAllText(destinationFilePath, jsonString);

                }
                _logger.WriteLogMessage("New JSON data created");
                return isNewJsonCreated = true;

            }
            catch (Exception ex)
            {
                _logger.WriteErrorMessage(ex);
                return isNewJsonCreated;
            }
        }

    }
}

