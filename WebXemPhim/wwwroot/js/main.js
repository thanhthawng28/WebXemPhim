function rateMovie(rating) {
    var movieId = @Model.MovieId; 
    $.ajax({
        type: "POST",
        url: "/Movie/RateMovie",
        data: { movieId: movieId, rating: rating },
        success: function () {
            var stars = $('.star');
            var selectedRating = parseInt(rating);
            stars.removeClass('selected');
            for (var i = 0; i < selectedRating; i++) {
                $(stars[i]).addClass('selected');
            }
        },
        error: function () {
        }
    });
}
function toggleFavorite() {
    var movieId = @Model.MovieId; 
    $.ajax({
        type: "POST",
        url: "/Movie/ToggleFavorite",
        data: { movieId: movieId },
        success: function (response) {
            if (response.isFavorite) {
                $('#favorite-icon').removeClass('far fa-heart').addClass('fas fa-heart');
            } else {
                $('#favorite-icon').removeClass('fas fa-heart').addClass('far fa-heart');
            }
        },
        error: function () {
        }
    });
}