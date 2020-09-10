set JAVA_CLASSES=MaterialWrapperGenerator.class ShaderProperty.class
set JAVA_SOURCES=MaterialWrapperGenerator.java ShaderProperty.java
set MANIFEST_FILE=manifest.mf
set JAR_FILE=../MaterialWrapperGenerator.jar

javac %JAVA_SOURCES%
jar cmf %MANIFEST_FILE% %JAR_FILE% %JAVA_CLASSES%
del %JAVA_CLASSES%
