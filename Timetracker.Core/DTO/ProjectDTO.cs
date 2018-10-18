using System;
using System.Collections.Generic;
using System.Text;
using TimeTracker.Core.Domain.Model;

namespace Timetracker.Core.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool Funded { get; set; }
        public static ProjectDTO ProjectDTOBuilder(Project project)
        {
            ProjectDTO projectDTO = new ProjectDTO()
            {
                Id = project.Id,
                Description = project.Description,
                Active = project.Active,
                Funded = project.Funded
            };

            return projectDTO;
        }
        public static Project ProjectBuilder(ProjectDTO projectDto)
        {
            Project project = new Project()
            {
                Description = projectDto.Description,
                Active = projectDto.Active,
                Id = projectDto.Id,
                Funded = projectDto.Funded
            };

            return project;
        }
    }
}
