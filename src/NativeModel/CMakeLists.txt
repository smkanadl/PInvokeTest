cmake_minimum_required(VERSION 3.10)

project(NativeModel VERSION 1.0 LANGUAGES C CXX)

add_library(NativeModel64 SHARED Model.cpp)
