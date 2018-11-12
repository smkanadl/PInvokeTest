set BOOST_ROOT=D:\libs\boost_1_65_0
set EIGEN3_INCLUDE_DIR=D:\libs\eigen-3.3.4

mkdir build\native\win\64
cd build\native\win\64
cmake -G"Visual Studio 15 2017 Win64" -DEIGEN3_INCLUDE_DIR:PATH=%EIGEN3_INCLUDE_DIR% ../../../..
cmake --build .
cd ..\..\..\..

mkdir build\native\win\32
cd build\native\win\32
cmake -G"Visual Studio 15 2017" -DEIGEN3_INCLUDE_DIR:PATH=%EIGEN3_INCLUDE_DIR% ../../../..
cmake --build .
cd ..\..\..\..
