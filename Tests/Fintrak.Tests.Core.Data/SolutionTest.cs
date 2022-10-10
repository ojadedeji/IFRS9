using System;
using System.ComponentModel.Composition;
using Fintrak.Data.Core.Contracts;
using System.Linq;
using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fintrak.Data.Core;
using Fintrak.Data.SystemCore;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Tests.Core.Data
{
    [TestClass]
    public class SolutionTest
    {
        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        [TestMethod]
        public void CreateTestMethod()
        {
            var solutionRepository = new SolutionRepository();

            var solution = new Solution { 
                Name = "CORE",
                Alias = "CORE",
                Active = true,
                Deleted =false,
                CreatedBy = "Test",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Test",
                UpdatedOn = DateTime.Now 
            };

            solution = solutionRepository.Add(solution);

            Assert.IsTrue(solution.EntityId > 0);
        }

        [TestMethod]
        public void UpdateTestMethod()
        {
            var solutionRepository = new SolutionRepository();

            var solution = solutionRepository.Get(2);

            solution.Alias = "Core Solution";
            solution.UpdatedBy = "Test";
            solution.UpdatedOn = DateTime.Now;

            solution = solutionRepository.Update(solution);

            solution = solutionRepository.Get(2);

            Assert.AreEqual(solution.Alias,"Core Solution");
        }

        [TestMethod]
        public void DeleteTestMethod()
        {
            var solutionRepository = new SolutionRepository();

            var solution = solutionRepository.Get(2);

            solutionRepository.Remove(solution);

            solution = solutionRepository.Get(2);

            Assert.IsNull(solution);
        }

        [TestMethod]
        public void BetByIdTestMethod()
        {
            var solutionRepository = new SolutionRepository();

            var solution = solutionRepository.Get().FirstOrDefault();

            var newSolution = solutionRepository.Get(solution.EntityId);

            Assert.AreEqual(newSolution.EntityId, solution.EntityId);
        }

        [TestMethod]
        public void BetAllTestMethod()
        {
            var solutionRepository = new SolutionRepository();

            var solutions = solutionRepository.Get();

            Assert.AreEqual(solutions.Count(),1);
        }
    }
}
