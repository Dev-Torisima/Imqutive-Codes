//Json.cpp
//Created by Torisima 2025
//License : https://github.com/Dev-Torisima/Imqutive-Codes/blob/main/LICENSE


#include "Json.h"


JsonNode::JsonNode()
{
	Key = "";
	Type = 0;

	Number = 0;
	String = "";
	Bool = false;
	Array = {};
	Object = {};
}

JsonNode::~JsonNode()
{

}

void JsonNode::Serialize(std::string& item)
{
	item += "\"" + Key + "\" : ";
	Serialize_NotKey(item);
}

void JsonNode::Serialize_NotKey(std::string& item)
{
	if (Type == JsonNodeType_Number)
	{
		item += std::to_string(Number);
	}
	else if (Type == JsonNodeType_String)
	{
		item += "\"" + String + "\"";
	}
	else if (Type == JsonNodeType_Bool)
	{
		if (Bool) item += "true";
		else item += "false";
	}
	else if (Type == JsonNodeType_Array)
	{
		item += "[ ";
		if (Array.size() != 0)
		{
			for (int i = 0; i < Array.size(); i++)
			{
				if (i != 0) item += ",";
				Array[i].Serialize_NotKey(item);
			}
		}
		item += " ]";
	}
	else if (Type == JsonNodeType_Object)
	{
		item += "{";
		if (Object.size() != 0)
		{
			for (int i = 0; i < Object.size(); i++)
			{
				if (i != 0) item += ",";
				Object[i].Serialize(item);
			}
		}

		item += " }";
	}
	else
	{
		item += "null";
	}
}

void JsonNode::Deserialize(const std::string& item)
{
	//Location
	int in = -1;
	int lek1 = 0;
	int lek2 = 0;
	int lek3 = 0;
	for (int i = 0; i < item.size(); i++)
	{
		if (item[i] == '\"')
		{
			if (i == 0)
			{
				if (lek1 == 1) lek1 -= 1;
				else lek1 += 1;
			}
			else
			{
				if (item[i - 1] == '\\')
				{

				}
				else
				{
					if (lek1 == 1) lek1 -= 1;
					else lek1 += 1;
				}
			}
		}
		else if (item[i] == '[') lek2 += 1;
		else if (item[i] == ']') lek2 -= 1;
		else if (item[i] == '{') lek3 += 1;
		else if (item[i] == '}') lek3 -= 1;
		else if (item[i] == ':')
		{
			if (lek1 == 0 && lek2 == 0 && lek3 == 0) in = i;
		}
	}

	//Key
	Key = "";
	if (in != -1)
	{
		std::vector<int> ss = {};
		for (int i = 0; i < in; i++)
		{
			if (item[i] == '\"')
			{
				if (i == 0)
				{
					ss.push_back(i);
				}
				else
				{
					if (item[i - 1] == '\\')
					{

					}
					else
					{
						ss.push_back(i);
					}
				}
			}
		}
		if (ss.size() != 0)
		{
			for (int i = 0; i < in; i++)
			{
				if (ss[0] < i && i < ss[ss.size() - 1])
				{
					Key += item[i];
				}
			}
		}

		//throw;
	}

	//Data
	std::string h = item.substr(in + 1);
	Deserialize_NotKey(h);
}

void JsonNode::Deserialize_NotKey(const std::string& item)
{
	std::string k = "";
	bool f = true;
	for (int i = 0; i < item.size(); i++)
	{
		if (item[i] == '\"')
		{
			if (i == 0)
			{
				if (f) f = false;
				else f = true;
			}
			else
			{
				if (item[i - 1] == '\\')
				{

				}
				else
				{
					if (f) f = false;
					else f = true;
				}
			}

			k += item[i];
		}
		else if (f)
		{
			if (item[i] != ' ') k += item[i];
		}
		else k += item[i];
	}

	if (k[0] == '\"')
	{
		Type = JsonNodeType_String;
		int ii = 0;
		for (int i = 0; i < k.size(); i++)
		{
			if (k[i] == '\"') ii = i;
		}

		if (k.size() > 2) k = k.substr(1, ii - 1);
		else k = "";

		String = k;
	}
	else if (k[0] == '[')
	{
		Type = JsonNodeType_Array;
		int ii = 0;
		for (int i = 0; i < k.size(); i++)
		{
			if (k[i] == '}') ii = i;
		}


		std::vector<JsonNode> kl = {};
		if (k.size() > 2)
		{
			k = k.substr(1, ii - 1);

			std::vector<std::string> cc = { };
			std::string ss = "";
			cc.push_back(ss);
			int yy = 0;
			bool f = true;

			int kaka1 = 0;
			int kaka2 = 0;

			for (int i = 0; i < k.size(); i++)
			{
				if (k[i] == '{')
				{
					kaka1 += 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '}')
				{
					kaka1 -= 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '[')
				{
					kaka2 += 1;
					cc[yy] += k[i];
				}
				else if (k[i] == ']')
				{
					kaka2 -= 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '\"')
				{
					if (i == 0)
					{
						if (f) f = false;
						else f = true;
					}
					else
					{
						if (item[i - 1] == '\\')
						{

						}
						else
						{
							if (f) f = false;
							else f = true;
						}
					}

					cc[yy] += k[i];
				}
				else if (f)
				{
					if (kaka1 > 0) cc[yy] += k[i];
					else if (kaka2 > 0) cc[yy] += k[i];
					else if (k[i] == ',')
					{
						std::string ss2 = "";
						cc.push_back(ss2);
						yy += 1;
					}
					else cc[yy] += k[i];
				}
				else cc[yy] += k[i];
			}
			for (int i = 0; i < cc.size(); i++)
			{
				kl.push_back(JsonNode{});
				kl[i].Deserialize(cc[i]);
			}
		}
		Array = kl;
	}
	else if (k[0] == '{')
	{
		Type = JsonNodeType_Object;
		int ii = 0;
		for (int i = 0; i < k.size(); i++)
		{
			if (k[i] == '}') ii = i;
		}


		std::vector<JsonNode> kl = {};
		if (k.size() > 2)
		{
			k = k.substr(1, ii - 1);

			std::vector<std::string> cc = { };
			std::string ss = "";
			cc.push_back(ss);
			int yy = 0;
			bool f = true;

			int kaka1 = 0;
			int kaka2 = 0;

			for (int i = 0; i < k.size(); i++)
			{
				if (k[i] == '{')
				{
					kaka1 += 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '}')
				{
					kaka1 -= 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '[')
				{
					kaka2 += 1;
					cc[yy] += k[i];
				}
				else if (k[i] == ']')
				{
					kaka2 -= 1;
					cc[yy] += k[i];
				}
				else if (k[i] == '\"')
				{
					if (i == 0)
					{
						if (f) f = false;
						else f = true;
					}
					else
					{
						if (item[i - 1] != '\\')
						{
							if (f) f = false;
							else f = true;
						}
					}

					cc[yy] += k[i];
				}
				else if (f)
				{
					if (kaka1 > 0) cc[yy] += k[i];
					else if (kaka2 > 0) cc[yy] += k[i];
					else if (k[i] == ',')
					{
						std::string ss2 = "";
						cc.push_back(ss2);
						yy += 1;
					}
					else cc[yy] += k[i];
				}
				else cc[yy] += k[i];
			}
			for (int i = 0; i < cc.size(); i++)
			{
				kl.push_back(JsonNode{});
				kl[i].Deserialize(cc[i]);
			}
		}

		Object = kl;

	}
	else if (k == "true")
	{
		Type = JsonNodeType_Bool;
		Bool = true;
	}
	else if (k == "false")
	{
		Type = JsonNodeType_Bool;
		Bool = false;
	}
	else if (k == "null")
	{
		Type = JsonNodeType_Null;
		//Null = Null;
	}
	else
	{
		try
		{
			Type = JsonNodeType_Number;
			Number = stod(k);
		}
		catch (const std::exception&)
		{
			Type = JsonNodeType_Null;
			//Null = Null;
		}
	}
}




template<class T>
void JsonSerialize(const T& in1, const JsonConverter<T>& in2, std::string& out)
{
	JsonNode jn = {};
	in2.Convert(in1, jn);
	jn.Serialize(out);
}

template<class T>
void JsonDeserialize(const std::string& in1, const JsonConverter<T>& in2, T& out)
{
	JsonNode jn = {};
	jn.Deserialize(in1);
	in2.Convert(jn, out);
}
