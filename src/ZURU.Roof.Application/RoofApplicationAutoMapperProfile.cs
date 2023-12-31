﻿using AutoMapper;
using ZURU.Roof.Books;

namespace ZURU.Roof;

public class RoofApplicationAutoMapperProfile : Profile
{
    public RoofApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
    }
}
