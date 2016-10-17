#
# Cookbook Name:: awesome_customers_windows
# Recipe:: lcm
#
# Copyright (c) 2016 The Authors, All Rights Reserved.
powershell_script 'Configure the LCM' do
  code <<-EOH
    Configuration ConfigLCM
    {
        Node "localhost"
        {
            LocalConfigurationManager
            {
                ConfigurationMode = "ApplyOnly"
                RebootNodeIfNeeded = $false
            }
        }
    }
    ConfigLCM -OutputPath "#{Chef::Config[:file_cache_path]}\\DSC_LCM"
    Set-DscLocalConfigurationManager -Path "#{Chef::Config[:file_cache_path]}\\DSC_LCM"
  EOH
  not_if '(Get-DscLocalConfigurationManager | select -ExpandProperty "ConfigurationMode") -eq "ApplyOnly"'
end
