cmake_minimum_required(VERSION 3.10)

cmake_policy(VERSION 3.12)

list(APPEND CMAKE_MODULE_PATH "${CMAKE_CURRENT_LIST_DIR}/" "${CMAKE_CURRENT_LIST_DIR}/cmake")

add_subdirectory(src)
