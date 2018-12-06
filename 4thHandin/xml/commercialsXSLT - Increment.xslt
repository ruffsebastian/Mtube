<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:c="http://my.company.com"
>
  <xsl:output method="xml" indent="yes"/>
  <xsl:param name="randomcommercialToDisplayPosition"></xsl:param>

  <xsl:template match="/">
    <commercials>
      <xsl:for-each select="c:commercials/c:commercial">
        <commercial>
          <xsl:variable name="id" select="position()" />
          <company>
            <xsl:value-of select="@company"/>
          </company>
          <webpage>
            <xsl:value-of select="c:webpage"/>
          </webpage>
          <logo>
            <xsl:value-of select="c:logo"/>
            <xsl:value-of select="c:ourlogo"/>
          </logo>
          <viewcount>
                <xsl:variable name="i" select="c:viewcount" />
            <xsl:choose>
              <xsl:when test="$id = $randomcommercialToDisplayPosition">
                
                  <xsl:value-of select="concat('$i = ', $i)"/>
               
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="c:viewcount"/>
              </xsl:otherwise>
            </xsl:choose>
            
          </viewcount>
        </commercial>
      </xsl:for-each>
    </commercials>
  </xsl:template>
</xsl:stylesheet>
