//Json.h
//Created by Torisima 2025
//License : https://github.com/Dev-Torisima/Imqutive-Codes/blob/main/LICENSE

#pragma once

#include <stdlib.h>
#include <memory.h>
#include <vector>
#include <iostream>
#include <fstream>
#include <sstream>


#define JsonNodeType_Null 0x00
#define JsonNodeType_Bool 0x03
#define JsonNodeType_Number 0x01
#define JsonNodeType_String 0x02
#define JsonNodeType_Object 0x10
#define JsonNodeType_Array 0x20

class JsonNode
{
public:
	JsonNode();
	~JsonNode();

public:
	std::string Key;
	unsigned char Type;

public:
	double Number;
	std::string String;
	bool Bool;
	std::vector<JsonNode> Array;
	std::vector<JsonNode> Object;


public:
	virtual void Serialize(std::string& item);
	virtual void Deserialize(const std::string& item);
	virtual void Serialize_NotKey(std::string& item);
	virtual void Deserialize_NotKey(const std::string& item);
};

template<class T>
class JsonConverter
{
public:
	virtual void Convert(const JsonNode& in, T& out) = 0;
	virtual void Convert(const T& in, JsonNode& out) = 0;
};

template<class T>
void JsonSerialize(const T& in1, const JsonConverter<T>& in2, std::string& out);

template<class T>
void JsonDeserialize(const std::string& in1, const JsonConverter<T>& in2, T& out);
