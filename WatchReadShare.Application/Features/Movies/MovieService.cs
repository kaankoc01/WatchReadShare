﻿using System.Net;
using AutoMapper;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Movies.Create;
using WatchReadShare.Application.Features.Movies.Dto;
using WatchReadShare.Application.Features.Movies.Update;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Movies
{
    public class MovieService(IMovieRepository movieRepository , IUnitOfWork unitOfWork, IMapper mapper) : IMovieService
    {
        public async Task<ServiceResult<MovieDto?>> GetByIdAsync(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie is null)
            {
                return ServiceResult<MovieDto?>.Fail("Film Bulunamadı.", HttpStatusCode.NotFound);
            }
            var movieDto = mapper.Map<MovieDto>(movie);
            return ServiceResult<MovieDto?>.Success(movieDto);
        }

        public async Task<ServiceResult<List<MovieDto>>> GetAllListAsync()
        {
            var movies = await movieRepository.GetAllAsync();
            var movieDto = mapper.Map<List<MovieDto>>(movies);
            return ServiceResult<List<MovieDto>>.Success(movieDto);
        }

        public async Task<ServiceResult<List<MovieDto>>> GetPagedAllList(int pageNumber, int pageSize)
        {
            // eğer ki pagenumber 1 , page size da 10 gelirse , ilk 10 kayıt demek
            // 1- 10 => ilk 10 kayıt , skip(0).Take(10) deriz.
            // 2- 10 => 11-20 kayıt , skip(10).Take(10) deriz.

            var movies = await movieRepository.GetAllPagedAsync(pageNumber, pageSize);

            var movieDto = mapper.Map<List<MovieDto>>(movies);

            return ServiceResult<List<MovieDto>>.Success(movieDto);

        }

        public async Task<ServiceResult<CreateMovieResponse>> CreateAsync(CreateMovieRequest request)
        {
            var anyProduct = await movieRepository.AnyAsync(x => x.Name == request.Name);
            if (anyProduct)
            {
                return ServiceResult<CreateMovieResponse>.Fail("Bu isimde bir film zaten var.", HttpStatusCode.BadRequest);
            }
            var movie = mapper.Map<Movie>(request);
            await movieRepository.AddAsync(movie);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateMovieResponse>.SuccessAsCreated(new CreateMovieResponse(movie.Id), $"api/movie/{movie.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateMovieRequest request)
        {
            var isMovieNameExist = await movieRepository.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (isMovieNameExist)
            {
                return ServiceResult.Fail("Bu isimde bir film zaten var.", HttpStatusCode.BadRequest);
            }
            var movie = mapper.Map<Movie>(request);
            movie.Id = request.Id;
            movieRepository.Update(movie);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var movie = await movieRepository.GetByIdAsync(id);
            if (movie is null)
            {
                return ServiceResult.Fail("Film Bulunamadı.", HttpStatusCode.NotFound);
            }
            movieRepository.Delete(movie);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}