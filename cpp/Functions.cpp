//Functions.cpp
//Created by Torisima 2025
//License : https://github.com/Dev-Torisima/Imqutive-Codes/blob/main/LICENSE

#include "Functions.h"

void BytesToString(const std::vector<unsigned char>& in, std::string& out)
{
    int n = in.size();
    out.clear();
    out.resize(n + 1);
    memcpy((void*)out.data(), in.data(), n);
    out[n] = '\0';
}

void StringToBytes(const std::string& in, std::vector<unsigned char>& out)
{
    out = {};
    out.resize(in.length());

    memcpy(out.data(), in.data(), in.length());
}

char LoadBinaryFile(const std::string& path, std::vector<unsigned char>& data)
{
    std::ifstream ifs(path, std::ios_base::in | std::ios_base::binary | std::ios_base::ate);
    if (ifs.fail()) return -3;


    std::streamsize fs = ifs.tellg();
    ifs.seekg(0, std::ifstream::beg);

    data.clear();
    data.resize(fs);
    ifs.read(reinterpret_cast<char*>(data.data()), fs);
    if (ifs.fail()) return -2;

    ifs.close();
    if (ifs.fail()) return -1;

    return 0;
}

char SaveBinaryFile(const std::string& path, const std::vector<unsigned char>& data)
{
    std::ofstream ofs(path, std::ios_base::out | std::ios_base::binary);
    if (ofs.fail()) return -3;

    ofs.write(reinterpret_cast<const char*>(data.data()), data.size());
    if (ofs.fail()) return -2;

    ofs.close();
    if (ofs.fail()) return -1;

    return 0;
}

template<class T>
void VectorSkip(const std::vector<T>& in, std::vector<T>& out, int offset)
{
    out = {};
    if (offset < in.size())
    {
        for (int i = offset; i < in.size(); i++)
        {
            out.push_back(in[i]);
        }
    }
    else
    {
        out = in;
    }
}

template<class T>
void VectorTake(const std::vector<T>& in, std::vector<T>& out, int size)
{
    out = {};
    if (size == 0) return;
    if (size <= in.size())
    {
        for (int i = 0; i < size; i++)
        {
            out[i] = in[i];
        }
    }
    else
    {
        out = in;
    }
}

/*template<class T>
void VectorSkipAndTake(const std::vector<T>& in, std::vector<T>& out, int offset, int size)
{
    out = std::vector<T>(size);
    if (size == 0) return;
    if (offset + size <= in.size())
    {
        for (int i = offset; i < size + offset; i++)
        {
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
}*/
