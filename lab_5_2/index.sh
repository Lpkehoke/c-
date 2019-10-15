#build cpp so file
echo "1. creating so lib"
cmake cpp/
make

#compile and run c#, i am lazy change mono on dotnet
echo "2. start c# compile"
csc lab_5_2.cs cs/*

if [ $? -eq 0 ]; then #check succses of compile
    echo "3. run"
    mono lab_5_2.exe
else
    echo "compile c# with error"
fi
