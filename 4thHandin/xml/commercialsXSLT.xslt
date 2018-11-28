<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                              xmlns:c="http://my.company.com"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    
    
    <xsl:for-each select="c:commercials/c:commercial">

      <xsl:value-of select="@company"/>
      <br/>
      <xsl:value-of select="c:webpage"/>
      <br/>
      <xsl:value-of select="c:logo"/>
      <xsl:value-of select="c:ourlogo"/>
      <br/>
      <xsl:value-of select="c:telephone"/>
      <xsl:value-of select="c:telephones/c:telephone"/>
      
      <br/>

      <br/><br/>
    </xsl:for-each>

  </xsl:template>

  

</xsl:stylesheet>
