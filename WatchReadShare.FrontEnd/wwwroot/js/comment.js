$(document).ready(function() {
    const commentForm = $('#addCommentForm');
    const toast = new bootstrap.Toast($('#commentToast'));
    
    // Yıldız derecelendirme sistemi
    $('.rating-star').hover(
        function() {
            const rating = $(this).data('rating');
            updateStars(rating);
        },
        function() {
            const currentRating = $('#ratingValue').val();
            updateStars(currentRating);
        }
    ).click(function() {
        const rating = $(this).data('rating');
        $('#ratingValue').val(rating);
        updateStars(rating);
        $(this).closest('.rating-section').find('.invalid-feedback').hide();
    });

    function updateStars(rating) {
        $('.rating-star').each(function(index) {
            $(this).toggleClass('fas', index < rating)
                   .toggleClass('far', index >= rating);
        });
    }

    // Form gönderimi
    commentForm.on('submit', function(e) {
        e.preventDefault();
        
        const movieId = $('#movieId').val();
        const commentText = $('#commentText').val().trim();
        const rating = parseInt($('#ratingValue').val());
        
        // Validasyon
        let isValid = true;
        
        if (!commentText) {
            $('#commentText').addClass('is-invalid');
            isValid = false;
        } else {
            $('#commentText').removeClass('is-invalid');
        }
        
        if (!rating) {
            $('.rating-section .invalid-feedback').show();
            isValid = false;
        } else {
            $('.rating-section .invalid-feedback').hide();
        }
        
        if (!isValid) return;

        // Loading durumu
        const submitButton = $(this).find('button[type="submit"]');
        const originalButtonText = submitButton.html();
        submitButton.html('<span class="spinner-border spinner-border-sm me-2"></span>Gönderiliyor...')
                   .prop('disabled', true);

        // API'ye gönderme
        $.ajax({
            url: '/Movie/AddComment',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({
                movieId: movieId,
                text: commentText,
                rating: rating
            }),
            success: function(response) {
                // Form temizleme
                $('#commentText').val('');
                $('#ratingValue').val('0');
                updateStars(0);
                
                // Başarı mesajı
                showToast('success', 'Yorumunuz başarıyla eklendi! Sayfa yenileniyor...');
                
                // Sayfayı yenile
                setTimeout(() => {
                    location.reload();
                }, 2000);
            },
            error: function(xhr) {
                let errorMessage = 'Bir hata oluştu. Lütfen tekrar deneyin.';
                
                if (xhr.status === 401) {
                    errorMessage = 'Yorum yapabilmek için giriş yapmalısınız.';
                } else if (xhr.responseJSON?.message) {
                    errorMessage = xhr.responseJSON.message;
                }
                
                showToast('error', errorMessage);
            },
            complete: function() {
                // Loading durumunu kaldır
                submitButton.html(originalButtonText)
                           .prop('disabled', false);
            }
        });
    });

    function showToast(type, message) {
        const toastEl = $('#commentToast');
        const toastBody = toastEl.find('.toast-body');
        
        toastEl.removeClass('bg-success bg-danger')
               .addClass(type === 'success' ? 'bg-success' : 'bg-danger');
        toastBody.text(message);
        
        toast.show();
    }
}); 