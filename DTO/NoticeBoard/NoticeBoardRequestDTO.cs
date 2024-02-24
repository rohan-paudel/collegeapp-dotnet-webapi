using System.ComponentModel.DataAnnotations;

namespace CollegeAppDotnetWebApi;

public class NoticeBoardRequestDTO
{
    [Required]
    public string Title { get; set; } = "";

    [Required]
    public string Notice { get; set; } = "";

    [Required]
    public int CollegeId { get; set; }

#pragma warning disable CS8618
    public IFormFile? File { get; set; }
#pragma warning restore CS8618

    public string? FileNameByDeveloper { get; set; }
}


// public async Task CreateNoticeBoard(NoticeBoardRequestDTO request)
//     {
//         var noticeBoard = new NoticeBoardModel
//         {
//             Title = request.Title,
//             Notice = request.Notice,
//             CollegeId = request.CollegeId
//         };

//         foreach (var subCourseId in request.IdsOfSubCourse)
//         {
//             var subCourse = await _dbContext.SubCourses.FindAsync(subCourseId);
//             if (subCourse != null)
//             {
//                 noticeBoard.SubCourses.Add(subCourse);
//             }
//             else
//             {
//                 // Handle invalid SubCourseId
//             }
//         }

//         _dbContext.NoticeBoards.Add(noticeBoard);
//         await _dbContext.SaveChangesAsync();
//     }
