yum install python27
yum install python-pip
pip install -U selenium

yum install Xvfb
pip install PyVirtualDisplay
env | grep DISPLAY

wget https://dl.google.com/linux/direct/google-chrome-stable_current_x86_64.rpm
sudo yum install ./google-chrome-stable_current_*.rpm
google-chrome &

yum install -f chromedriver chromium xorg-x11-server-Xvfb

cd /home/dev
wget https://chromedriver.storage.googleapis.com/2.35/chromedriver_linux64.zip
unzip chromedriver_linux64.zip





servis kurulumu
sudo nano /etc/systemd/system/zingat.service

[Unit]
Description=zingat

[Service]
WorkingDirectory=/var/service
ExecStart=/usr/bin/dotnet /var/service/ConsoleApp1.dll
Restart=always
RestartSec=10  # Restart service after 10 seconds if dotnet service crashes
SyslogIdentifier=zingat
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target

chmod 644 /etc/systemd/system/zingat.service

systemctl enable zingat.service
systemctl start zingat.service
systemctl status zingat.service

journalctl --unit zingat.service --follow  