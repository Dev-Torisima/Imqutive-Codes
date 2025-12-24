size_t sjis_to_utf8_size(const unsigned char *s)
{
    size_t total = 0;
    while (*s) 
    {
        unsigned char c = *s++;
        if (c < 0x80) {total += 1;continue;}
        if (0xA1 <= c && c <= 0xDF) {total += 3;continue;}

        if (*s == 0) {total += 3;break;}

        unsigned char c2 = *s++;
        if (c == 0x82 && 0x9F <= c2 && c2 <= 0xF1) {total += 3;continue;}
        if (c == 0x83 && 0x40 <= c2 && c2 <= 0x96) {total += 3;continue;}

        total += 3;
    }
    return total + 1;
}

int sjis_to_utf8_out(unsigned char *buf, unsigned int code)
{
    if (code <= 0x7F) {buf[0] = code;return 1;}
    if (code <= 0x7FF) {buf[0] = 0xC0 | (code >> 6);buf[1] = 0x80 | (code & 0x3F);return 2;}
    if (code <= 0xFFFF) {buf[0] = 0xE0 | (code >> 12);buf[1] = 0x80 | ((code >> 6) & 0x3F);buf[2] = 0x80 | (code & 0x3F);return 3;}
    buf[0] = 0xF0 | (code >> 18);buf[1] = 0x80 | ((code >> 12) & 0x3F);buf[2] = 0x80 | ((code >> 6) & 0x3F);buf[3] = 0x80 | (code & 0x3F);return 4;
}

void sjis_to_utf8_write(const unsigned char *s, unsigned char *out)
{
    unsigned char *p = out;
    while (*s) 
    {
        unsigned char c = *s++;
        if (c < 0x80) {*p++ = c;continue;}
        if (0xA1 <= c && c <= 0xDF) {p += sjis_to_utf8_out(p, (0xFF61 + (c - 0xA1)));continue;}

        if (*s == 0) {p += sjis_to_utf8_out(p, 0x25A1);break;}

        unsigned char c2 = *s++;
        if (c == 0x82 && 0x9F <= c2 && c2 <= 0xF1) {p += sjis_to_utf8_out(p, 0x3041 + (c2 - 0x9F));continue;}
        if (c == 0x83 && 0x40 <= c2 && c2 <= 0x96) {p += sjis_to_utf8_out(p, 0x30A1 + (c2 - 0x40));continue;}
        p += sjis_to_utf8_out(p, 0x25A1);
    }
    *p = '\0';
}

void sjis_to_utf8(const char* str)
{
    const unsigned char *sjis = (unsigned char*)str;
    size_t size = sjis_to_utf8_size(sjis);
    unsigned char utf8[size];
    sjis_to_utf8_write(sjis, utf8);
    printf("%s", utf8);
}
