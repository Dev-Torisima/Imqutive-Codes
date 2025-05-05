#pragma once

#include <vector>
#include <string>
#include <iostream>
#include <fstream>


void BytesToString(const std::vector<unsigned char>& in, std::string& out);

void StringToBytes(const std::string& in, std::vector<unsigned char>& out);

char LoadBinaryFile(const std::string& path, std::vector<unsigned char>& data);

char SaveBinaryFile(const std::string& path, const std::vector<unsigned char>& data);

template<class T>
void VectorSkip(const std::vector<T>& in, std::vector<T>& out, int offset);

template<class T>
void VectorTake(const std::vector<T>& in, std::vector<T>& out, int size);

template<class T>
void VectorSkipAndTake(const std::vector<T>& in, std::vector<T>& out, int offset, int size)
{
    out = std::vector<T>(size);
    if (size == 0) return;
    if (offset + size <= in.size())
    {
        for (int i = offset; i < size + offset; i++)
        {
            //throw;
            out[i - offset] = in[i];
        }
    }
    else
    {
        out = in;
    }
}

template<class T>
void VectorReverse(std::vector<T>& in)
{
    int y = in.size();
    std::vector<T> out = std::vector<T>(y);
    for (int i = 0; i < y; i++)
    {
        out[i] = in[y - 1 - i];
    }
    in = out;
}
