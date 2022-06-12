using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jibble.DTOs
{
    public class PagedRequestDto : IValidatableObject
    {
        /// <summary>
        /// Default value: 10.
        /// </summary>
        public static int DefaultMaxResultCount { get; set; } = 10;

        /// <summary>
        /// Maximum possible value of the <see cref="MaxResultCount"/>.
        /// Default value: 1,000.
        /// </summary>
        public static int MaxMaxResultCount { get; set; } = 1000;

        /// <summary>
        /// Maximum result count should be returned.
        /// </summary>
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = DefaultMaxResultCount;
        
        [Range(0, int.MaxValue)]
        public virtual int SkipCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaxResultCount > MaxMaxResultCount)
            {
                yield return new ValidationResult($"MaxResultCount Exceeded. Please provide a value less than {MaxMaxResultCount}");
            }
        }
    }
}
