<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:c="http://my.company.com"
>
  <xsl:output method="xml" indent="yes"/>
  
  
  <xsl:template match="/">
    <commercials>
    <xsl:for-each select="c:commercials/c:commercial">
      <commercial>
        <xsl:attribute name="company">
          <xsl:value-of select="@company"/>
        </xsl:attribute>
        <xsl:attribute name="webpage">
          <xsl:value-of select="c:webpage"/>
        </xsl:attribute>
        <xsl:attribute name="logo">
          <xsl:value-of select="c:logo"/>
        <xsl:value-of select="c:ourlogo"/>
        </xsl:attribute>
      </commercial>
    </xsl:for-each>
    </commercials>    
  </xsl:template>
</xsl:stylesheet>
