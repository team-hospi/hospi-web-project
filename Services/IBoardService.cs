using hospi_web_project.Models;
using System.Collections.Generic;

namespace hospi_web_project.Services
{
    public interface IBoardService
    {
        public List<BoardViewModel> GetBoardList();
        public BoardViewModel GetBoardDetail(int no);
        public void WriteBoard(BoardViewModel model);
        public void UpdateBoard(BoardViewModel model);
        public void DeleteBoard(int no);
        public List<BoardViewModel> SearchBoardList(string searchText);
    }
}
