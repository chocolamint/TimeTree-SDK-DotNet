using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTreeSdkDotNet.Entities;

/// <summary>
/// Provides additional information about a TimeTree calendar.
/// </summary>
/// <param name="Name">The calendar name.</param>
/// <param name="Description">The calendar description.</param>
/// <param name="Color">The color of the calendar.</param>
/// <param name="Order">The order of the calendar.</param>
/// <param name="ImageUrl">The <see cref="Uri"/> of the calendar's image.</param>
/// <param name="CreatedAt">The time when the calendar created.</param>
public record TimeTreeCalendarAttribute(string Name, string Description, string Color, int Order, Uri ImageUrl, DateTimeOffset CreatedAt);
