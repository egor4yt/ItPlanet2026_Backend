using Launchpad.Application.Exceptions;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Commands.Skills.AttachEmployee;

public class AttachEmployeeSkillsCommandHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<AttachEmployeeSkillsCommandRequest>
{
    public async Task Handle(AttachEmployeeSkillsCommandRequest request, CancellationToken cancellationToken)
    {
        var employee = await applicationDbContext.Employees
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken);
        if (employee == null) throw new BadRequestException("EmployeeNotFound");

        employee.Skills.Clear();

        foreach (var skill in request.Skills.Where(x => !x.Id.HasValue))
        {
            employee.Skills.Add(new Skill
            {
                IsSystemTag = false,
                Title = skill.Title
            });
        }

        var existsSkillsIds = request.Skills
            .Where(s => s.Id.HasValue)
            .Select(s => s.Id);

        var existsSkills = await applicationDbContext.Skills
            .Where(x => existsSkillsIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var skill in existsSkills)
        {
            employee.Skills.Add(skill);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}