﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightaplusplus.Models;
using System.ComponentModel.DataAnnotations;

namespace Lightaplusplus.BisLogic
{
    public class AssignmentAdder
    {

        private readonly Lightaplusplus.Data.Lightaplusplus_SystemContext _context;
        public AssignmentAdder(Lightaplusplus.Data.Lightaplusplus_SystemContext context)
        {
            _context = context;
        }

        public void AddAssignment(int SectionId, string AssignmentTitle, string AssignmentDescription, DateTime AssignmentDueDateTime, int AssignmentMaxPoints, char AssignmentSubmissionType)
        {
            Models.Assignments newAssignment = new Models.Assignments
            {
                SectionId = SectionId,
                AssignmentTitle = AssignmentTitle,
                AssignmentDescription = AssignmentDescription,
                AssignmentDueDateTime = AssignmentDueDateTime,
                AssignmentMaxPoints = AssignmentMaxPoints,
                AssignmentSubmissionType = AssignmentSubmissionType
            };
            newAssignment.AssignmentCreationDate = DateTime.Now;
            _context.Assignments.Add(newAssignment);
            _context.SaveChanges();
            return;
        }

        public bool CheckAssignment(string AssignmentTitle, int SectionId)
        {
            try
            {
                var assignment = _context.Assignments
                    .Where(t => t.AssignmentTitle == AssignmentTitle)
                    .Where(d => d.SectionId == SectionId)
                    .First();
                return assignment != null;
            }
            catch (Exception)
            {
                // log exception
                return false;
            }
        }

        public void RemoveAssignmet(string AssignmentTitle, int SectionId)
        {
            while (CheckAssignment(AssignmentTitle, SectionId))
            {
                _context.Assignments.Remove(_context.Assignments.Where(t => t.AssignmentTitle == AssignmentTitle).Where(s => s.SectionId == SectionId).First());
                _context.SaveChanges();
            }

            return;
        }
    }
}
