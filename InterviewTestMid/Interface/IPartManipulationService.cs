using System;
using InterviewTestMid.Model;

namespace InterviewTestMid.Interface
{
	public interface IPartManipulationService
	{

        List<Part>? ListAndLogPartsMaterialDesc(string materialDescription);

        Part ModifyPartWeight(Part part, double newWeight);

        List<Part>? LoadBaseJsonPartsData();

        bool CreateNewPartJsonFile(List<Part> parts);
    }
}

