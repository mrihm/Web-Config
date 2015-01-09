<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html"  indent="yes" />

    <xsl:param name="test_mode" select="'false'" />
    <xsl:param name="to_list" select="''" />
    <xsl:param name="cc_list" select="''" />
    <xsl:param name="bcc_list" select="''" />

    <xsl:template match="/employee" >
        <body style="text-align: center;">
            <xsl:if test="$test_mode='true'">
                <table style="width:700px; font-family: calibri, arial, sans-serif; border: solid 2px #6D6E71; margin: 0 auto; text-align:left;" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 10px 5px 5px; font-size: 16px; font-weight: bold">
                                <xsl:text disable-output-escaping="yes">* * * T e s t&amp;nbsp;&amp;nbsp;&amp;nbsp;E m a i l * * *</xsl:text>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 0 15px 5px; font-size: 14px;">
                                <h2 style="margin: 0 0 0.75em 0; font-size: 16px;">
                                    Original recipient(s):
                                </h2>
                                <strong>
                                    <xsl:text disable-output-escaping="yes">To:&amp;nbsp;</xsl:text>
                                </strong>
                                <xsl:value-of select="$to_list"/>
                                <br/>
                                <strong>
                                    <xsl:text disable-output-escaping="yes">Cc:&amp;nbsp;</xsl:text>
                                </strong>
                                <xsl:value-of select="$cc_list"/>
                                <br/>
                                <strong>
                                    <xsl:text disable-output-escaping="yes">Bcc:&amp;nbsp;</xsl:text>
                                </strong>
                                <xsl:value-of select="$bcc_list"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br/>
            </xsl:if>

            <div style="width: 700px; font: bold 11px arial; margin: 0 auto; padding: 5px 10px; text-align: center;">
                *** Please do not respond to this email ***
            </div>
            <table style="width:700px; font-family: calibri, arial, sans-serif; border:solid 2px #6D6E71; margin:0 auto; text-align:left;" cellpadding="0" cellspacing="0">
                <xsl:call-template name="header"/>
                <xsl:call-template name="body"/>
            </table>
        </body>
    </xsl:template>

    <xsl:template name="header">
        <thead>
            <tr valign="bottom">
                <td align="left" style="border-bottom: solid 1px #6D6E71; padding: 15px 0 5px 15px;">
                    <img src="https://www.kai-datapipe-nmh.com/images/logo/nm-banner.png" alt="NM Medicine Logo" />
                </td>
                <td align="right" style="border-bottom: solid 1px #6D6E71; padding: 15px 15px 5px 0;">
                    <h1 style="margin: 0; font-size: 22px; font-family: calibri, Arial; color: #61468B;">
                        Post Exposure Follow-Up Compliance
                    </h1>
                </td>
            </tr>
        </thead>
    </xsl:template>

    <xsl:template name="body">
        <tbody>
            <tr>
                <td colspan="2" style="padding: 20px 15px 5px; font-size: 16px;">
                    <h2 style="margin: 0 0 0.75em 0; font-size: 16px;">
                        Dear <xsl:value-of select="name"/>
                    </h2>

                    <p style="margin: 0.25em 0 1em 0; line-height: 1.25em;">
                        Our records indicate that you are due for your 10 week communicable disease exposure follow-up on <xsl:value-of select="deadline" />.
                        Please contact Corporate Health immediately to schedule your appointment. 
                    </p>

                    <p style="margin: 0.25em 0 1em 0; line-height: 1.25em;">
                        <strong style="text-decoration: underline">Corporate Health- NMH Downtown Location</strong><br/>
                        Located in the Arkes Family Pavilion<br/>
                        676 N. St. Clair, Suite 900
                        <ul style="margin: 0; padding: 0">
                            <li style="margin: 0 0 10px 25px; padding: 0;">Monday-Friday 7am - 5pm</li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">1st Tuesday of the month 7am - 7pm</li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">3rd Saturday of the month 7am - 10am (excluding holiday weekends)</li>
                        </ul>
                        <span style="text-decoration: underline">
                            <a href="https://nmpg-srvr-tbs01.nmpg.nmh.org/default.aspx" style="color:#472F92">Click here</a> to schedule an appointment
                        </span>
                        or call 312-926-8282
                    </p>

                    <p style="margin: 0.25em 0 1em 0; line-height: 1.25em;">
                        <strong style="text-decoration: underline">Corporate Health- North Market</strong><br/>
                        Located next to the Emergency Department at NLFH<br/>
                        <ul style="margin: 0; padding: 0">
                            <li style="margin: 0 0 10px 25px; padding: 0;">Monday: 7:30am - 4:00pm </li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">Tuesday: 7:30am - 4:00pm </li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">Wednesday: 7:30am - 6:00pm</li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">Thursday: 7:30am - 4:00pm</li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">Friday: 7:30am - 4:00pm</li>
                            <li style="margin: 0 0 10px 25px; padding: 0;">Every other Saturday: 9:00am - 11:00am</li>
                        </ul>
                        Call 847-535-6997 to schedule
                    </p>

                    <p style="margin: 0.25em 0 1em 0; line-height: 1.25em;">
                        <strong>Please be advised that the post exposure follow-up is a requirement for your continued employment. If you do not comply with 
                        this requirement, you will automatically be removed from the schedule 1 day after your deadline.</strong>
                    </p>


                    <p style="margin: 0.25em 0 1em 0; line-height: 1.25em;">
                        If you have already completed your tests and feel you received this notification in error, please contact Occupational Health &amp; 
                        Employee Safety at 312-926-7238.
                    </p>
                </td>
            </tr>
        </tbody>

    </xsl:template>

</xsl:stylesheet>
