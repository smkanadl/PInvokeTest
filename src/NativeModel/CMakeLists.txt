project(NativeModel VERSION 1.0 LANGUAGES C CXX)

if(MSVC)
    if("${CMAKE_EXE_LINKER_FLAGS}" MATCHES "x64")
        set(CMAKE_ARCHITECTURE_TYPE "64")
    else()
        set(CMAKE_ARCHITECTURE_TYPE "32" )
    endif()
else()
    if(CMAKE_SIZEOF_VOID_P EQUAL 8)
        set(CMAKE_ARCHITECTURE_TYPE "64")
    else()
        set(CMAKE_ARCHITECTURE_TYPE "32" )
    endif()
endif()

find_package(Eigen3 REQUIRED)

set(Boost_USE_STATIC_LIBS ON)
set(Boost_USE_MULTITHREADED ON)
#set(Boost_DEBUG TRUE)
find_package(boost REQUIRED)

set(Lib NativeModel${CMAKE_ARCHITECTURE_TYPE})

add_library(${Lib} SHARED
	Bisection.cpp
	Bisection.hpp
	Model.cpp
	SystemOfLinearEquations.cpp
	SystemOfLinearEquations.hpp
)

target_link_libraries(${Lib} PRIVATE Eigen3::eigen3 Boost::boost)

add_custom_command(TARGET ${Lib} POST_BUILD COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:${Lib}> ${CMAKE_BINARY_DIR}/$<CONFIG> VERBATIM)
