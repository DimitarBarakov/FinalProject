﻿using FootballApp.Data.Models;
using FootballApp.ViewModels.Club;

namespace FootballApp.Services.Interfaces
{
    public interface IClubService
    {
        public Task<ClubPageViewModel?> GetClubByIdAsync(int clubId);

        public Task AddToFavoritesAsync(int clubId, string userId);

        public Task<bool> DoesClubExistsByIdAsync(int houseId);

        public Task AddClubAsync(AddClubViewModel model);
    }
}
