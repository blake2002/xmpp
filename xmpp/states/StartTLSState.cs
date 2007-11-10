//XMPP .NET Library Copyright (C) 2006 Dieter Lunn
//
//This library is free software; you can redistribute it and/or modify it under
//the terms of the GNU Lesser General Public License as published by the Free
//Software Foundation; either version 3 of the License, or (at your option)
//any later version.
//
//This library is distributed in the hope that it will be useful, but WITHOUT
//ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
//FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
//
//You should have received a copy of the GNU Lesser General Public License along
//with this library; if not, write to the Free Software Foundation, Inc., 59
//Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;
using System.Xml;
using xmpp;
using xmpp.core;
using xmpp.registries;
using xmpp.common;

namespace xmpp.states
{
	public class StartTLSState : State
	{
		private ProtocolState _state;
		
		public StartTLSState(ProtocolState state)
		{
			_state = state;
		}
		
		public override void Execute (object data)
		{
			TagEventArgs e = data as TagEventArgs;
			TagRegistry reg = TagRegistry.Instance;
			
			if (e.Tag == "proceed")
			{
				_state.Socket.StartSecure();
				Stream stream = (Stream)reg.GetTag("stream", new XmlQualifiedName("stream", Namespaces.STREAM), new XmlDocument());
				stream.Version = "1.0";
				stream.To = _state.Socket.Hostname;
				stream.NS = "jabber:client";
				_state.Socket.Write("<?xml version='1.0' encoding='UTF-8'?>" + stream.StartTag());
			}
		}

	}
}
