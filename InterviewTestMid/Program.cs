using InterviewTestMid.Interface;
using InterviewTestMid.Model;
using InterviewTestMid.Service;
using InterviewTestMid.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace InterviewTestMid
{
    internal class Program
    {
        private readonly ILogger _logger;
        private readonly IPartManipulationService _partManipulationService;


        public Program(ILogger logger, IPartManipulationService partManipulationService)
        {
            _logger = logger;
            _partManipulationService = partManipulationService;
        }

        private void DoWork()
        {

            _logger.WriteLogMessage("Doing some JSON tasks...");

            string partMaterialDesc = "FOIL";
            
            var partsForDesc = _partManipulationService.ListAndLogPartsMaterialDesc(partMaterialDesc);

            if(partsForDesc is null || (partsForDesc.Count < 1)) {
                _logger.WriteLogMessage($"No Parts present for provided Material Description - {partMaterialDesc}");
                return;
            }

            var weightModifiedPart = _partManipulationService.ModifyPartWeight(partsForDesc.First(), 2.00000000);

            var modifiedParts = new List<Part> { weightModifiedPart };

            var doesModifiedPartJsonCreated = (_partManipulationService.CreateNewPartJsonFile(modifiedParts))
               ? $"New Json with Modified part {modifiedParts} - Created!"
               : $"New Json with Modified part {modifiedParts} - Not Created!";

            _logger.WriteLogMessage(doesModifiedPartJsonCreated);

            _logger.WriteLogMessage("Finished doing some JSON tasks.");

        }


        static void Main(string[] args)
        {
            // DI container
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<IDebugWrapper, DebugWrapper>()
                .AddSingleton<IFileSystem, FileSystem>()
                .AddTransient<IPartManipulationService, PartManipulationService>()
                .AddSingleton<Program>()
                .BuildServiceProvider();

            var program = serviceProvider.GetRequiredService<Program>();

            program.Run();
        }

        private void Run()
        {
            DoWork();
        }
    }
}